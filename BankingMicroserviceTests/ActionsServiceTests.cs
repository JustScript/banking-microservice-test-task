using BankingMicroservice.Models;
using BankingMicroservice.Services;

namespace BankingMicroserviceTests
{
    public class ActionsServiceTests
    {
        public static IEnumerable<object[]> TestDataSet
        {
            get
            {
                // Dla karty PREPAID w statusie CLOSED aplikacja powinna zwrócić akcje: ACTION3, ACTION4, ACTION9.
                yield return new object[]
                {
                    new CardDetails("Card1", CardType.Prepaid, CardStatus.Closed, true),
                    new List<string>(){ "ACTION3", "ACTION4", "ACTION9" }
                };

                // Dla karty CREDIT w statusie BLOCKED aplikacja powinna zwrócić akcje: ACTION3, ACTION4, ACTION5, ACTION6 (jeżeli pin nadany), ACTION7 (jeżeli pin nadany), ACTION8, ACTION9
                yield return new object[]
                {
                    new CardDetails("Card1", CardType.Credit, CardStatus.Blocked, true), // With PIN
                    new List<string>(){ "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION7", "ACTION8", "ACTION9" }
                };

                // Dla karty CREDIT w statusie BLOCKED aplikacja powinna zwrócić akcje: ACTION3, ACTION4, ACTION5, ACTION6 (jeżeli pin nadany), ACTION7 (jeżeli pin nadany), ACTION8, ACTION9
                yield return new object[]
                {
                    new CardDetails("Card1", CardType.Credit, CardStatus.Blocked, false), // Without PIN
                    new List<string>(){ "ACTION3", "ACTION4", "ACTION5", "ACTION8", "ACTION9" }
                };
            }
        }

        [Theory]
        [MemberData(nameof(TestDataSet))]
        public void PdfExamplesShouldPass(CardDetails card, List<string> expected)
        {
            // Arrange
            IActionsService service = new ActionsService();

            // Act
            var output = service.GetAllowedActions(card);

            // Assert
            Assert.Equal(expected, output);
        }

    }
}