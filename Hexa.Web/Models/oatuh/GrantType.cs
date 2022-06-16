﻿using Microsoft.EntityFrameworkCore;

namespace Hexa.Web.Models.oatuh
{
    public enum GrantName
    {
        AUTHORIZATION, PASSWORD, IMPLICITE, CLIENT_CREDENTIAL, PKCE
    }
    public class GrantType
    {
        public int id { get; set; }
        public GrantName Name { get; set; }
        public string Description { get; set; }


        public List<GrantType> GrantTypes { get; } = new();
    }
}
