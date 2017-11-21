using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        [TestMethod]
        public void TestMethod_obl()
        {
            var form = new Event();

            string regEmail = @"\A[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\z";

            var falseResEmail = form.RegTrue("ry5yy6hu6h", regEmail);
            var trueResEmail = form.RegTrue("o9a7@mail.ru", regEmail);

            Assert.IsFalse(falseResEmail);
            Assert.IsTrue(trueResEmail);
        }

        [TestMethod]
        public void TestMethod_put()
        {
            var form = new Menu();

            var trueResPassword = form.LogIN("moder1", "moder1");
            var falseResPassword = form.LogIN(null, null);

            Assert.IsNotNull(trueResPassword);
            Assert.IsNull(falseResPassword);
        }

        [TestMethod]
        public void TestMethod_kom()
        {
            Event form = new Event();

            var falseCheck = form.WriteTel("8950");
            var trueCheck = form.WriteTel("89504820227");

            Assert.IsNotNull(trueCheck);
            Assert.IsNull(falseCheck);
        }

    }
}
