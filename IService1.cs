using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Scrap_Service
{

    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_Offer> GetOffers();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_Manufacture> GetManufactures(DTO_Manufacture m);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_Manufacture> GetManufacturesById(DTO_Manufacture m);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_Manufacture> GetManufacturesByPartial(DTO_Manufacture m);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_User> GetUsers();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_User> GetIndUser(DTO_User u);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_User> LoginUser(DTO_Login login);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        DTO_User VerifyUserLogin(DTO_Login uL);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_User> RegisterUser(DTO_User u);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_Register> RegisterUser_Bekka(DTO_Register newUser);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_WishListItems> GetWishListItems(DTO_User user);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_Offer> GetOffersByPartDesc(DTO_Part p);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_Transaction> GetTransactions(DTO_User user);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_Offer> GetOffersByUser(DTO_User user);

        /* [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        [return: MessageParameter(Name = "Data")]
        List<DTO_Transaction> PurchaseOffer_Checkout(DTO_Offer o, DTO_User u); */
    }

}
