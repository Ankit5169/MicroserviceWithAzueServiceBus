using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessEntities.Enums;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MessagesManagement;
namespace MT.OnlineRestaurant.BusinessLayer
{
    public class PaymentActions : IPaymentActions
    {
        private readonly IPaymentDbAccess _paymentDbAccess;

        private readonly IMessageSender _busSender;
        public PaymentActions(IPaymentDbAccess paymentDbAccess, IMessageSender busSender )
        {
            _paymentDbAccess = paymentDbAccess;
            _busSender = busSender;
        }

        public int MakePaymentForOrder(PaymentEntity orderPaymentDetails)
        {
            //1.check  stock

            //2.place order
            var status =
            _paymentDbAccess.MakePaymentForOrder(new DataLayer.Context.TblOrderPayment()
            {
                TblFoodOrderId = orderPaymentDetails.OrderId,
                TblPaymentTypeId = orderPaymentDetails.PaymentTypeId,
                Remarks = orderPaymentDetails.Remarks,
                TblCustomerId = orderPaymentDetails.CustomerId,
                TblPaymentStatusId = (int)Status.Initiated,
                RecordTimeStampCreated = DateTime.Now
            });

            //3.update stock

            //Make Azure Call
            _busSender.SendMessagesAsync(orderPaymentDetails.OrderId.ToString(), "ap-test-topic");
            
            return 0;
        }

        public int UpdatePaymentAndOrderStatus(UpdatePaymentEntity orderPaymentDetails)
        {
            return _paymentDbAccess.UpdatePaymentAndOrderStatus(new DataLayer.Context.TblOrderPayment()
            {
                Id = orderPaymentDetails.PaymentId,
                TransactionId = orderPaymentDetails.TransactionReferenceNo,
                TblPaymentStatusId = orderPaymentDetails.PaymentStatusId
            });
        }
    }
}
