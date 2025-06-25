#nullable enable

using System.ComponentModel.DataAnnotations;

namespace DataContract.Models;

public class EventRegistration
{
    public Guid Id { get; set; }
    
    public Guid EventId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string? Pronouns { get; set; }
    
    public bool OptInForCommunication { get; set; }
    
    public DateTime CreatedAt { get; set; }
}