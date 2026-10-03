using Library.ApplicationCore;
using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace Library.UnitTests.Infrastructure.JsonLoanRepositoryTests;

public class GetLoanTest
{
	private readonly ILoanRepository _mockLoanRepository;
	private readonly JsonLoanRepository _jsonLoanRepository;
	private readonly IConfiguration _configuration;
	private readonly JsonData _jsonData;

	public GetLoanTest()
	{
		_mockLoanRepository = Substitute.For<ILoanRepository>();
		_configuration = new ConfigurationBuilder().Build();
		_jsonData = new JsonData(_configuration);
		_jsonLoanRepository = new JsonLoanRepository(_jsonData);
	}

	[Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns loan when ID is found")]
	public async Task GetLoan_ReturnsLoanWhenIdIsFound()
	{
		// Arrange
		var loanId = 1;
		var expectedLoan = new Loan { Id = loanId, BookItemId = 17, 
		PatronId = 42, LoanDate = DateTime.UtcNow.AddDays(-3), 
        DueDate = DateTime.Now.AddDays(7)    };
		_mockLoanRepository.GetLoan(loanId).Returns(expectedLoan);

		// Act
		var actualLoan = await _jsonLoanRepository.GetLoan(loanId);

		// Assert
		Assert.NotNull(actualLoan);
		Assert.Equal(expectedLoan.Id, actualLoan!.Id);
	}
}
