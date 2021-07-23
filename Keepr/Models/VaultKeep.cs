namespace Keepr.Models
{
  public class VaultKeep
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
    public Vault Vault { get; set; }
    public Keep Keep { get; set; }
    public Account Creator { get; set; }
  }
}