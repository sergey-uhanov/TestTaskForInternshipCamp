﻿namespace TestTaskForInternshipCamp.Server
{
    public class BlobResponceDto
    {
        public BlobResponceDto()
        {
            Blob = new BlobDto();
        }
        public string? status { get; set; }
        public bool Error { get; set; }
        public BlobDto Blob { get; set; }

    }
}
