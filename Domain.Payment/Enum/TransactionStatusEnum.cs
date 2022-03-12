namespace Payment.DataAccess.Enum
{
    public enum TransactionStatusEnum
    {
        //requires_payment_method, requires_confirmation, requires_action, processing, requires_capture, canceled, or succeeded


        ///// <summary>
        ///// Starting status for all invoices; at this point, the invoice can still be edited. <br/>
        ///// Finalize it to open, or delete it if it’s a one-off invoice.
        ///// </summary>
        //Draft = 1,
        ///// <summary>
        ///// The invoice has been finalized, and is now awaiting payment from the customer. <br/>
        ///// It can no longer be edited.	Send, void, mark uncollectible, or pay the invoice.
        ///// </summary>
        //Open,
        ///// <summary>
        ///// This invoice was paid.
        ///// </summary>
        //Paid,
        ///// <summary>
        ///// This invoice was a mistake, and should be canceled.
        ///// </summary>
        //Void,
        ///// <summary>
        ///// It’s unlikely that this invoice will be paid, and it should be treated as bad debt in reports.
        ///// </summary>
        //Uncollectible
    }
}