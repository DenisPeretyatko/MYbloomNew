using BloomService.Domain.Models.Requests;
using BloomService.Domain.Models.Responses;
using Common.Logging;
using System;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;

namespace Sage.WebApi.Infratructure.Service.Implementation
{
    public class ServiceAuthorization : IServiceAuthorization
    {
        private readonly IServiceManagement _serviceManagement;

        private readonly ILog _log = LogManager.GetLogger(typeof(ServiceAuthorization));

        public ServiceAuthorization(IServiceManagement serviceManagement)
        {
            _serviceManagement = serviceManagement;
        }
        public AuthorizationResponse Authorization(AuthorizationRequest model)
        {
            _log.InfoFormat("Authorization: Name = {0} Password: {1}", model.Name, model.Password);
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
                {
                    _log.InfoFormat("Failed authorization");
                    return null;
                }
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
                _log.InfoFormat("Authorization success: Mail: {0}, Type: {1}, Id: {2}", response.Mail, response.Type, response.Id);
                return response;
            }
            catch(Exception ex)
            {
                _log.InfoFormat("Error authorization: {0}", ex.Message);
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