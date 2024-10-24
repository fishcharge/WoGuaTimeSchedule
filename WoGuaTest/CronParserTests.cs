using CronParse;

namespace WoGuaTest
{
    [TestFixture]
    public class CronParserTests
    {
        [Test]
        public void TestIsValid()
        {
            Assert.Multiple(() =>
            {
                Assert.That(ValidCron.IsValid("* * * * *"), Is.True);
                Assert.That(ValidCron.IsValid("0 0 1 1 0"), Is.True);
                Assert.That(ValidCron.IsValid("*/15 0 1,15 * 1-5"), Is.True);
                Assert.That(ValidCron.IsValid("60 * * * *"), Is.False);
                Assert.That(ValidCron.IsValid("* * * *"), Is.False);
                Assert.That(ValidCron.IsValid("* * * * * *"), Is.False);
            });
        }

    }
}
