namespace Hexa.Data.DTOs
{
	public class NewApplicationDTO
	{
		public ApplicationDTO application { get; set; }
		public List<RedirectURIDTO> redirectURIs { get; set; }
		
	}
}
