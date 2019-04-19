using NUnit.Framework;
using Orders.BLL;
using Orders.Data;
using Orders.Models;
using Orders.Models.Interfaces;
using Orders.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tests
{
    public class FlooringTests
    {
        [TestFixture]
        public class LookUpOrderFromMgr_Tests
        {
            ////// ////// ////// ////// ////// ////// ////// ////// //////
            ////// Build 5 instances of a Lookup Order Queryaccount /////
            [TestCase(2022, 2, 2, 2, true)]
            //expects pass since this is a valid date and order number

            [TestCase(2002, 2, 2, 1, false)]
            //expects a fail since this is not a valid date

            [TestCase(2002, 2, 2, 9, false)]
            //expects fail since this is not a valid order number

            ////// ////// ////// ////// ////// ////// ////// ////// ////// /////
            ////// Defines rule and defines each varible for one account //////
            ////// AND defines expectedResult                            /////
            public void LookupOrderFileTest
                (int orderYr, int orderMnth, int orderDay, int orderNum, bool expectedResult)
            {
                ////// ////// ////// ////// ////// ////// /////  /////  /////  /////
                ////// Creates instance of repo to call the method being tested ///
                //IDeposit deposit = new NoLimitDepositRule();
                //OrderManager orderRepo = new OrderManagerFactory.Create();
                //_manager = OrderManagerFactory.Create();
                OrderManager manager = new OrderManager(new OrderTestRepo2());
                //IDeposit deposit = new NoLimitDepositRule();

                ////// ////// ////// ////// ////// ///
                ////// Creats 1 account object //////
                DateTime testDate = new DateTime();

                testDate = new DateTime(orderYr, orderMnth, orderDay);
                int testOrderNum = orderNum;

                ////// ////// ////// ////// ////// //////
                ////// Call the method being tested /////
                //            AccountDepositResponse response = deposit.Deposit(account, amount);

                OrderLookupResponse response = new OrderLookupResponse();
                response = manager.LookupOrder(testDate, orderNum);
                Assert.AreEqual(expectedResult, response.Success);
            }
        }
               
        public class NextOrder_Tests
        {
            ////// ////// ////// ////// ////// ////// ////// ////// //////
            ////// Build 5 instances of a Lookup Order Queryaccount /////
            [TestCase(2022, 2, 2, 3, true)]
            //expects pass since the next order should be3

            [TestCase(2002, 2, 2, 9, false)]
            //expects fail since this date does not exist in repo

            public void LookupOrderFileTest
                (int orderYr, int orderMnth, int orderDay, int orderNum, bool expectedResult)
            {
                //OrderManager orderRepo = new OrderManager(new OrderTestRepo2());
                //IDeposit deposit = new NoLimitDepositRule();

                DateTime testDate = new DateTime();

                testDate = new DateTime(orderYr, orderMnth, orderDay);

                ////// ////// ////// ////// ////// //////
                ////// Call the method being tested /////
                OrderLookupResponse response = new OrderLookupResponse();
                
                OrderManager manager = new OrderManager(new OrderTestRepo2());

                int nextOrder = manager.NextOrderID(testDate);
                bool actualResult = (nextOrder == orderNum);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        public class LoadOrdersFromMgr_Tests
        {
            ////// ////// ////// ////// ////// ////// ////// ////// //////
            ////// Build 5 instances of a Lookup Order Queryaccount /////
            [TestCase(2022, 2, 2, true)]
            //expects pass since this is a valid date

            [TestCase(2033, 2, 2, false)]
            //expects fail since this is not a valid date

            public void LookupOrderFileTest
                (int orderYr, int orderMnth, int orderDay, bool expectedResult)
            {
                //OrderManager orderRepo = new OrderManager(new OrderTestRepo2());
                //IDeposit deposit = new NoLimitDepositRule();

                DateTime testDate = new DateTime();

                testDate = new DateTime(orderYr, orderMnth, orderDay);

                ////// ////// ////// ////// ////// //////
                ////// Call the method being tested /////
                OrderLookupResponse response = new OrderLookupResponse();
                
                OrderManager manager = new OrderManager(new OrderTestRepo2());

                OrdrLkpResponses GetOrders = new OrdrLkpResponses();
                GetOrders = manager.LoadOrders(testDate);

                bool actualResult = GetOrders.Success;
               
                Assert.AreEqual(expectedResult, actualResult);
                
            }
        }

        public class LookUpOrderFromRepo_Tests
        {
            ////// ////// ////// ////// ////// ////// ////// ////// //////
            ////// Build 5 instances of a Lookup Order Queryaccount /////
            [TestCase(2022, 2, 2, 2, true)]
            //expects pass since this is a valid date and order number

            [TestCase(2002, 2, 2, 1, false)]
            //expects pass since this is not a valid date

            [TestCase(2002, 2, 2, 9, false)]
            //expects pass since this is not a valid order number

            ////// ////// ////// ////// ////// ////// ////// ////// ////// /////
            ////// Defines rule and defines each varible for one account //////
            ////// AND defines expectedResult from Repo, not Mgr         /////
            public void LookupOrderFileTest
                (int orderYr, int orderMnth, int orderDay, int orderNum, bool expectedResult)
            {
                ////// ////// ////// ////// ////// ////// /////  /////  /////  /////
                ////// Creates instance of repo to call the method being tested ///
                //IDeposit deposit = new NoLimitDepositRule();
                //OrderManager orderRepo = new OrderManagerFactory.Create();
                //_manager = OrderManagerFactory.Create();

                //IOrderRepo orderRepo = new FileRepo();
                IOrderRepo orderRepo = new OrderTestRepo2();
                //IDeposit deposit = new NoLimitDepositRule();

                ////// ////// ////// ////// ////// ///
                ////// Creats 1 account object //////
                DateTime testDate = new DateTime();

                testDate = new DateTime(orderYr, orderMnth, orderDay);
                int testOrderNum = orderNum;

                ////// ////// ////// ////// ////// //////
                ////// Call the method being tested /////
                //            AccountDepositResponse response = deposit.Deposit(account, amount);

                OrderLookupResponse response = new OrderLookupResponse();

                Order TestOrder = orderRepo.LoadOrder(testDate, orderNum);
                response.Success = (TestOrder != null);
                Assert.AreEqual(expectedResult, response.Success);
            }
        }


        public class LoadOrdersFromRepo_Tests
        {
            ////// ////// ////// ////// ////// ////// ////// ////// //////
            ////// Build 5 instances of a Lookup Order Queryaccount /////
            [TestCase(2022, 2, 2, true)]
            //expects pass since this is a valid date

            [TestCase(2033, 2, 2, false)]
            //expects fail since this is not a valid date

            public void LookupOrderFileTest
                (int orderYr, int orderMnth, int orderDay, bool expectedResult)
            {
                DateTime testDate = new DateTime();

                testDate = new DateTime(orderYr, orderMnth, orderDay);

                ////// ////// ////// ////// ////// //////
                ////// Call the method being tested /////

                //OrderManager manager = new OrderManager(new OrderTestRepo2());
                OrderTestRepo2 Repo = new OrderTestRepo2();

                OrdrLkpResponses GetOrders = new OrdrLkpResponses();
                GetOrders = Repo.LoadOrders(testDate);
 
                bool actualResult = GetOrders.Success;

                Assert.AreEqual(expectedResult, actualResult);
                 
            }
        }

    }
}