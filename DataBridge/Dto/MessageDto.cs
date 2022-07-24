using System.ComponentModel.DataAnnotations;

namespace DataBridge.Dto
{
    public record MessageDto()
    {
        [Required]
        public string MessageToSend { get; set; }   
        
    }
}
