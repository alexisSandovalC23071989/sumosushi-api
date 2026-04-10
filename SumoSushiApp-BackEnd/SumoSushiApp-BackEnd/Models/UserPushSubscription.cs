namespace SumoSushiApp_BackEnd.Models
{
    public class UserPushSubscription
    {
        public long IdUsuario { get; set; }
        public PushSubscriptionModel? Subscription { get; set; }
    }
}
