using System.Collections.Generic;

namespace WabAPI.Dtos
{
    public class MailDetailsDto
    {
        public MailItemDto Mail { get; set; }
        public DocumentDto Body { get; set; }
        public IEnumerable<DocumentDto> Documents { get; set; }
    }
}
