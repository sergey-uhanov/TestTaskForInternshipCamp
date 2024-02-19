using FakeItEasy;
using FluentAssertions;
using learningBlob;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestTaskForInternshipCamp.Server;
using TestTaskForInternshipCamp.Server.Controllers;
using Xunit;

namespace TestTaskForIntershipCamp.Test
{
    public class FilesControllerTests
    {
        [Fact]
        public async Task Upload_Should_Return_Ok_Result_With_Response()
        {
            // Arrange
            var fileService = A.Fake<BlobStorageService>();
            var controller = new FilesController(fileService);
            
            var fileContent = new byte[] { 0x12, 0x34, 0x56 };
            var fileName = "testFile.txt";
            var file = new FormFile(new MemoryStream(fileContent), 0, fileContent.Length, "Data", fileName);

            var model = new FileUploadModel
            {
                File = file,
                Email = "test@example.com"
            };

            var expectedResponse = new BlobResponceDto
            {
                status = "File uploaded successfully",
                Error = false,
                Blob = new BlobDto
                {
                    Uri = "https://example.com/blob",
                    Name = "example_blob"
                }
            };
            A.CallTo(() => fileService.UpLoadAsync(A<FileUploadModel>._)).Returns(expectedResponse);

            // Act
            var result = await controller.Upload(model) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(expectedResponse);
        }        
    }
}
