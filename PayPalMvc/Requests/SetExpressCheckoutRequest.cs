using PayPalMvc.Enums;
using System.Collections.Generic;
namespace PayPalMvc
{
    /// <summary>
    /// Represents a transaction registration that is sent to PayPal. 
    /// This should be serialized using the HttpPostSerializer.
    /// </summary>
    public class SetExpressCheckoutRequest : CommonRequest
    {
        readonly PaymentAction paymentAction;
        readonly SolutionType solutionType;

        readonly string currencyCode;
        readonly decimal totalAmount;
        readonly decimal totalItemAmount;
        readonly decimal taxAmount;
        readonly string paymentDescription;
        readonly string trackingReference;
        readonly string noShipping;
        readonly string allowNote;
        readonly string reqConfirmShipping;
        
        readonly List<ExpressCheckoutItem> items;

        readonly string cancelUrl;
        readonly string serverURL;
        readonly string email;

        // See ITransactionRegister for parameter descriptions
        public SetExpressCheckoutRequest(string uniqueCode, string allowNote, string noShipping, string currencyCode, decimal totalAmount, decimal totalItemAmount, decimal taxAmount, string paymentDescription, string trackingReference, string serverURL, List<ExpressCheckoutItem> purchaseItems = null, string userEmail = null)
        {
            base.method = RequestType.SetExpressCheckout;
            this.paymentAction = PaymentAction.Sale;
            this.solutionType = SolutionType.Sole;

            this.currencyCode = currencyCode;
            this.totalAmount = totalAmount;
            this.totalItemAmount = totalItemAmount;
            this.taxAmount = taxAmount;
            this.paymentDescription = paymentDescription;
            this.trackingReference = trackingReference;
            this.noShipping = noShipping;
            this.allowNote = allowNote;
            this.reqConfirmShipping = "0";

            this.serverURL = serverURL;
            this.items = purchaseItems;
            this.email = userEmail;

            //Add unique code in cancel url
            this.cancelUrl = string.Format(Configuration.Current.CancelAction, uniqueCode);
        }

        
        //AddressType Fields
        public string PAYMENTREQUEST_0_PAYMENTACTION
        {
            get { return paymentAction.ToString(); }
        }

        public string PAYMENTREQUEST_0_CURRENCYCODE
        {
            get { return currencyCode; }
        }

        public string PAYMENTREQUEST_0_AMT
        {
            get { return totalAmount.ToString("f2"); }
        }

        public string PAYMENTREQUEST_0_DESC
        {
            get { return paymentDescription; }
        }

        public string PAYMENTREQUEST_0_INVNUM
        {
            get { return trackingReference; }
        }

        public string PAYMENTREQUEST_0_TAXAMT 
        {
            get { return taxAmount.ToString("f2"); }
        }

        public string PAYMENTREQUEST_0_ITEMAMT
        {
            get { return totalItemAmount.ToString(); }
        }


        //SetExpressCheckout Request Fields
        public string NOSHIPPING
        {
            get { return noShipping; }
        }

        public string ALLOWNOTE
        {
            get { return allowNote; }
        }

        public string SOLUTIONTYPE
        {
            get { return solutionType.ToString(); }
        }

        public string REQCONFIRMSHIPPING
        {
            get { return reqConfirmShipping; }
        }
        
        [Optional]
        public string EMAIL
        {
            get { return email; }
        }

        public string RETURNURL
        {
            get { return serverURL + Configuration.Current.ReturnAction; }
        }

        public string CANCELURL
        {
            get { return serverURL + cancelUrl; }
        }

        // Optional List of Items in this purchase
        [Optional]
        public List<ExpressCheckoutItem> Items
        {
            get { return this.items; }
        }
    }
}