using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Interfaces;
using OnlineShop.ViewModels.Payment;

namespace OnlineShop.Controllers;

[Authorize]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    // GET: /Payment/Start/5
    public async Task<IActionResult> Start(int orderId)
    {
        var payment = await _paymentService.GetByOrderIdAsync(orderId);

        if (payment == null)
        {
            var paymentId = await _paymentService.CreatePaymentAsync(orderId);

            payment = await _paymentService.GetByIdAsync(paymentId);

            if (payment == null)
                return NotFound();
        }

        var vm = new PaymentStartVm
        {
            OrderId = payment.OrderId,
            Amount = payment.Amount
        };

        return View(vm);
    }

    // POST: /Payment/Pay
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(int orderId)
    {
        var payment = await _paymentService.GetByOrderIdAsync(orderId);

        if (payment == null)
            return NotFound();

        // فعلاً شبیه‌سازی موفق بودن پرداخت
        return RedirectToAction(
            nameof(Callback),
            new
            {
                paymentId = payment.Id,
                success = true
            });
    }

    // GET: /Payment/Callback
    public async Task<IActionResult> Callback(
        int paymentId,
        bool success)
    {
        if (!success)
        {
            await _paymentService.FailPaymentAsync(paymentId);

            return RedirectToAction(nameof(Failed));
        }

        var transactionId = Guid.NewGuid().ToString("N");

        var result = await _paymentService
            .ConfirmPaymentAsync(paymentId, transactionId);

        if (!result)
            return RedirectToAction(nameof(Failed));

        return RedirectToAction(
            nameof(Success),
            new { paymentId });
    }

    // GET: /Payment/Success
    public async Task<IActionResult> Success(int paymentId)
    {
        var payment = await _paymentService.GetByIdAsync(paymentId);

        if (payment == null)
            return NotFound();

        var vm = new PaymentResultVm
        {
            IsSuccess = true,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            TransactionId = payment.TransactionId,
            Message = "Payment completed successfully."
        };

        return View(vm);
    }

    // GET: /Payment/Failed
    public IActionResult Failed()
    {
        var vm = new PaymentResultVm
        {
            IsSuccess = false,
            Message = "Payment failed."
        };

        return View(vm);
    }
}