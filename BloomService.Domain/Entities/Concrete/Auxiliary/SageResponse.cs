namespace BloomService.Domain.Entities.Concrete.Auxiliary
{
    using System.Collections.Generic;

    public class SageResponse
    {
        public IEnumerable<SageEntity> Entities { get; set; }

        public SageEntity Entity { get; set; }

        public string ErrorMassage { get; set; }

        public bool IsSucceed { get; set; }

        public IEnumerable<string> Strings { get; set; }
    }
}