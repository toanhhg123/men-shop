using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Male.Models;
using Male.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;

namespace Male.Controllers
{
    [Authorize(Roles = "CUSTOMER")]
    public class CheckOutController : Controller
    {
        private readonly ILogger<CheckOutController> _logger;
        private readonly MyDBContext _dbContext;

        private readonly IUserService _userService;
        private readonly IToastNotification _toast;

        private IConfigurationRoot ConfigRoot;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public CheckOutController(ILogger<CheckOutController> logger,
        MyDBContext dBContext, IUserService userService,
        IToastNotification toastNotification, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dbContext = dBContext;
            _userService = userService;
            _toast = toastNotification;
            ConfigRoot = (IConfigurationRoot)configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<bool> Order(List<Cart> carts)
        {

            try
            {
                foreach (var item in carts)
                {
                    await _dbContext.Orders.AddAsync(new Order()
                    {
                        account = item.Account,
                        product = item.product,
                        Quantity = item.Quantity,
                        isConfirm = false,
                        status = "dang cho xac nhan",
                        desc = ""
                    });

                    _dbContext.Carts.Remove(item);
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }
        public async Task<IActionResult> Index()
        {
            var info = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.id == _userService.getUserId());
            ViewBag.info = info;
            return View(await _dbContext.Carts.
            Include(x => x.product).
            Include(x => x.Account).
            Where(x => x.Account.id == _userService.getUserId()).
            ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var carts = await _dbContext.Carts.
            Include(x => x.product).
            Include(x => x.Account).
            Where(x => x.Account.id == _userService.getUserId()).
            ToListAsync();

            bool order = await Order(carts);
            ViewBag.Message = "Thanh toán thành công hóa đơn ";
            ViewBag.ResponseCode = "00";

            return View("CheckOutSuccess");
        }


        public async Task<IActionResult> Payment()
        {
            var info = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.id == _userService.getUserId());
            if (info == null) return BadRequest();
            string vnp_Returnurl = $"{Request.Scheme}://{Request.Host}/checkout/CheckOutSuccess"; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigRoot["appSettings:Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigRoot["appSettings:tmnCode"]; //Ma website
            string vnp_HashSecret = ConfigRoot["appSettings:hashSecret"]; //

            var carts = await _dbContext.Carts.
            Include(x => x.product).
            Include(x => x.Account).
            Where(x => x.Account.id == _userService.getUserId()).
            ToListAsync();

            List<Order> Orders = new List<Order>();
            foreach (var item in carts)
            {
                Orders.Add(new Order()
                {
                    account = item.Account,
                    product = item.product,
                    Quantity = item.Quantity,
                    isConfirm = false,
                    desc = ""
                });
            }
            double price = (Orders.Sum(x => x.product.Price * x.Quantity));
            price = price * 2300000;

            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", price.ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTimeOffset.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(_httpContextAccessor));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + Guid.NewGuid().ToString());
            vnpay.AddRequestData("vnp_OrderType", "Shopping"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            // vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            // //Billing
            // vnpay.AddRequestData("vnp_Bill_Mobile", txt_billing_mobile.Text.Trim());
            // vnpay.AddRequestData("vnp_Bill_Email", txt_billing_email.Text.Trim());
            var fullName = info.userName.ToString();
            vnpay.AddRequestData("vnp_Bill_Address", info.address ?? "no address");
            vnpay.AddRequestData("vnp_Bill_State", "");

            // Invoice
            vnpay.AddRequestData("vnp_Inv_Phone", info.phoneNumber ?? "no phone");
            vnpay.AddRequestData("vnp_Inv_Email", info.email);
            // vnpay.AddRequestData("vnp_Inv_Customer",    );
            vnpay.AddRequestData("vnp_Inv_Address", info.address ?? "no address");
            // vnpay.AddRequestData("vnp_Inv_Company", txt_inv_company.Text);
            vnpay.AddRequestData("vnp_Inv_Taxcode", "2000");
            vnpay.AddRequestData("vnp_Inv_Type", "type");
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return Redirect(paymentUrl);



        }


        public async Task<IActionResult> CheckOutSuccess()
        {
            var queris = _httpContextAccessor.HttpContext?.Request.Query;
            if (queris != null && queris.Count > 0)
            {
                string hashSecret = ConfigRoot["appSettings:hashSecret"]; //Chuỗi bí mật
                VnPayLibrary VNPay = new VnPayLibrary();
                var vnpayData = queris;

                //lấy toàn bộ dữ liệu được trả về
                foreach (var s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s.Key))
                    {
                        VNPay.AddResponseData(s.Key, s.Value.ToString());
                    }
                }
                var orderId = queris.FirstOrDefault(x => x.Key == "vnp_TxnRef").Value; //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(queris.FirstOrDefault(x => x.Key == "vnp_TransactionNo").Value); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = queris.FirstOrDefault(x => x.Key == "vnp_ResponseCode").Value; //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = queris.FirstOrDefault(x => x.Key == "vnp_SecureHash").Value; //hash của dữ liệu trả về


                bool checkSignature = VNPay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?
                if (checkSignature)
                {
                    ViewBag.ResponseCode = vnp_ResponseCode;
                    if (vnp_ResponseCode == "00")
                    {

                        var carts = await _dbContext.Carts.
                                    Include(x => x.product).
                                    Include(x => x.Account).
                                    Where(x => x.Account.id == _userService.getUserId()).
                                    ToListAsync();
                        await Order(carts);
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            return View();
        }




    }
}