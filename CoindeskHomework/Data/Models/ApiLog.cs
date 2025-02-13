using System.ComponentModel.DataAnnotations;

namespace CoindeskHomework.Data.Models
{
    public class ApiLog
    {
        [Key]
        public int Id { get; set; }

        public string? RequestPath { get; set; } 
        public string? Method { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }
        public int StatusCode { get; set; } 
        public string? RequestHeaders { get; set; }
        public string? ResponseHeaders { get; set; }
        
        public int ExecutionSeconds { get; set; }   

        public DateTime Timestamp { get; set; } 

    }

}
