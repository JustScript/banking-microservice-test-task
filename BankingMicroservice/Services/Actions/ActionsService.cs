using BankingMicroservice.Models;
using BankingMicroservice.Extensions;

namespace BankingMicroservice.Services
{
    public class ActionsService : IActionsService
    {
        public IEnumerable<string> GetAllowedActions(CardDetails cardDetails)
        {
            var allowedActionRules = GetAllowedActionRules(cardDetails);

            return allowedActionRules
                .Where(rule => rule.IsMatch(cardDetails))
                .Select(rule => rule.Action.ToString().ToUpper());
        }

        private IEnumerable<ActionPermissions> GetAllowedActionRules(CardDetails cardDetails)
        {
            return new List<ActionPermissions>
            {
                new ActionPermissions(
                    Action: CardActions.Action1,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: false,
                    Inactive: false,
                    Active: true,
                    Restricted: false,
                    Blocked: false,
                    Expired: false,
                    Closed: false
                ),
                new ActionPermissions(
                    Action: CardActions.Action2,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: false,
                    Inactive: true,
                    Active: false,
                    Restricted: false,
                    Blocked: false,
                    Expired: false,
                    Closed: false
                ),
                new ActionPermissions(
                    Action: CardActions.Action3,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: true,
                    Inactive: true,
                    Active: true,
                    Restricted: true,
                    Blocked: true,
                    Expired: true,
                    Closed: true
                ),
                new ActionPermissions(
                    Action: CardActions.Action4,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: true,
                    Inactive: true,
                    Active: true,
                    Restricted: true,
                    Blocked: true,
                    Expired: true,
                    Closed: true
                ),
                new ActionPermissions(
                    Action: CardActions.Action5,
                    Prepaid: false,
                    Debit: false,
                    Credit: true,
                    Ordered: true,
                    Inactive: true,
                    Active: true,
                    Restricted: true,
                    Blocked: true,
                    Expired: true,
                    Closed: true
                ),
                new ActionPermissions(
                    Action: CardActions.Action6,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: cardDetails.IsPinSet ? true : false,   // TAK - ale jak nie ma PIN to NIE
                    Inactive: cardDetails.IsPinSet ? true : false,  // TAK - ale jak nie ma PIN to NIE
                    Active: cardDetails.IsPinSet ? true : false,    // TAK - ale jak nie ma PIN to NIE
                    Restricted: false,
                    Blocked: cardDetails.IsPinSet ? true : false,   // TAK - jeżeli PIN nadany
                    Expired: false,
                    Closed: false
                ),
                new ActionPermissions(
                    Action: CardActions.Action7,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: !cardDetails.IsPinSet ? true : false,  // TAK - jeżeli brak PIN
                    Inactive: !cardDetails.IsPinSet ? true : false, // TAK - jeżeli brak PIN
                    Active: !cardDetails.IsPinSet ? true : false,   // TAK - jeżeli brak PIN
                    Restricted: false,
                    Blocked: cardDetails.IsPinSet ? true : false,   // TAK - jeżeli PIN nadany
                    Expired: false,
                    Closed: false
                ),
                new ActionPermissions(
                    Action: CardActions.Action8,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: true,
                    Inactive: true,
                    Active: true,
                    Restricted: false,
                    Blocked: true,
                    Expired: false,
                    Closed: false
                ),
                new ActionPermissions(
                    Action: CardActions.Action9,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: true,
                    Inactive: true,
                    Active: true,
                    Restricted: true,
                    Blocked: true,
                    Expired: true,
                    Closed: true
                ),
                new ActionPermissions(
                    Action: CardActions.Action10,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: true,
                    Inactive: true,
                    Active: true,
                    Restricted: false,
                    Blocked: false,
                    Expired: false,
                    Closed: false
                ),
                new ActionPermissions(
                    Action: CardActions.Action11,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: false,
                    Inactive: true,
                    Active: true,
                    Restricted: false,
                    Blocked: false,
                    Expired: false,
                    Closed: false
                ),
                new ActionPermissions(
                    Action: CardActions.Action12,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: true,
                    Inactive: true,
                    Active: true,
                    Restricted: false,
                    Blocked: false,
                    Expired: false,
                    Closed: false
                ),
                new ActionPermissions(
                    Action: CardActions.Action13,
                    Prepaid: true,
                    Debit: true,
                    Credit: true,
                    Ordered: true,
                    Inactive: true,
                    Active: true,
                    Restricted: false,
                    Blocked: false,
                    Expired: false,
                    Closed: false
                ),
            };
        }
    }
}