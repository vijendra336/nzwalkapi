﻿namespace NZWalk.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }   
        //nullable type ? property 
        public string? RegionImageUrl { get; set; }



    }
}
