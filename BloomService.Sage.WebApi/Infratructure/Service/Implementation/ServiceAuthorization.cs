using BloomService.Domain.Models.Requests;
using BloomService.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Web;

namespace Sage.WebApi.Infratructure.Service.Implementation
{
    public class ServiceAuthorization : IServiceAuthorization
    {
        IServiceManagement _serviceManagement;
        public ServiceAuthorization(IServiceManagement serviceManagement)
        {
            _serviceManagement = serviceManagement;
        }
        public AuthorizationResponse Authorization(AuthorizationRequest model)
        {
            var response = new AuthorizationResponse();
            try
            {
                var credential = new NetworkCredential(model.Name, model.Password, "bloom.local");
                var ldapConnection = new LdapConnection("BRSISVR01");
                ldapConnection.AuthType = AuthType.Kerberos;
                ldapConnection.Credential = credential;
                ldapConnection.Bind();

                var technician = CheckGroup(ldapConnection, "Technicians", model.Name);
                var manager = CheckGroup(ldapConnection, "ServiceDept", model.Name);
                if (technician == null && manager == null)
                    return null;
                SearchResponse searchResponse;
                if (technician != null)
                {
                    response.Type = AuthorizationType.Technician;
                    searchResponse = technician;
                }
                else
                {
                    response.Type = AuthorizationType.Manager;
                    searchResponse = manager;
                }


                //var pageResponse = (PageResultResponseControl)searchResponse.Controls[0];
                var mail = string.Empty;
                foreach (var result in searchResponse.Entries)
                {
                    var searchResultEntry = (SearchResultEntry)result;
                    System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                    var attribute = searchResultEntry.Attributes["mail"];
                    if (attribute != null)
                        mail = (string)attribute.GetValues(typeof(string)).FirstOrDefault();
                }
                if(!string.IsNullOrEmpty(mail))
                {
                    response.Mail = mail;
                    response.Id = GetId(mail);
                }
                return response;
            }
            catch
            {
                return null;
            }

        }

        private SearchResponse CheckGroup(LdapConnection ldapConnection, string nameGroup, string nameUser)
        {
            var filterString = string.Format("(&(objectClass=user)(sAMAccountName={0})(memberOf=CN={1},OU=SBSUsers,OU=Users,OU=MyBusiness,DC=bloom,DC=local))", nameUser, nameGroup);
            var searchRequest = new SearchRequest
                        //("CN=Users,DC=bloom,DC=local",
                        ("OU=SBSUsers,OU=Users,OU=MyBusiness,DC=bloom,DC=local",
                        filterString,
                        System.DirectoryServices.Protocols.SearchScope.Subtree,
                        "mail");
            var searchResponse = (SearchResponse)ldapConnection.SendRequest(searchRequest);
            if (searchResponse.Entries.Count == 0)
                return null;
            return searchResponse;
        }

        private string GetId(string mail)
        {
            var id = string.Empty;
            var technicians = _serviceManagement.Employees();
            var technician = technicians.FirstOrDefault(x => x.Email == mail);
            if (technician != null)
                id = technician.Employee;
            return id;
        }
    }
}