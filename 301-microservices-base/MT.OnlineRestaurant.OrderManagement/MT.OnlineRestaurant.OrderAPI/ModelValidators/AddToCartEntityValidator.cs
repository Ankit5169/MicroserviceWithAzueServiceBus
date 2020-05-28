using FluentValidation;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.Utilities;

namespace MT.OnlineRestaurant.OrderAPI.ModelValidators
{
    /// <summary>
    /// Food order model validator
    /// </summary>
    public class AddToCartEntityValidator : AbstractValidator<AddToCartEntity>
    {
        private readonly IAddToCartActions _iAddToCartActions;
        /// <summary>
        /// Constructor
        /// </summary>
        public AddToCartEntityValidator(int UserId, string UserToken, IAddToCartActions addToCartActions)
        {
            _iAddToCartActions = addToCartActions;

            RuleFor(m => m)
                .NotEmpty()
                .NotNull()
                .Must(r => BeAValidRestaurant(r, UserId, UserToken)).When(p => p.RestaurantId != 0).WithMessage("Invalid Restaurant");

            RuleFor(m => m)
                .NotEmpty()
                .NotNull();
                //.Must(r => BeAValidItemOrder(r, UserId, UserToken)).When(p => p.RestaurantId != 0).WithMessage("Invalid order");

            RuleFor(m => m.CustomerId)
                .NotEmpty()
                .NotNull();
            //   .Must(BeAValidCustomer).When(p => p.Customer_Id != 0).WithMessage("Invalid Customer");

            RuleFor(m => m.OrderMenuDetails)
                .NotNull()
                .Must(m => m.Count > 0)
                .WithMessage("Menu items cannot be empty");

            RuleForEach(m => m.OrderMenuDetails).SetValidator(new OrderMenuValidator());
        }

        /// <summary>
        /// Make a service call to fetch all restaurants and validate between them
        /// </summary>
        /// <param name="addToCartEntity">AddToCartEntity</param>
        /// <param name="UserId">UserId</param>
        /// <param name="UserToken">UserToken</param>
        /// <returns>Boolean whether specified restaurant is valid or invalid</returns>
        public bool BeAValidRestaurant(AddToCartEntity addToCartEntity, int UserId, string UserToken)
        {
            bool IsValidRestaurant = _iAddToCartActions.IsValidRestaurantAsync(addToCartEntity, UserId, UserToken).GetAwaiter().GetResult();
            return IsValidRestaurant;
        }
        /// <summary>
        /// Make a service call to check for Item availability
        /// </summary>
        /// <param name="addToCartEntity">AddToCartEntity</param>
        /// <param name="UserId">UserId</param>
        /// <param name="UserToken">UserToken</param>
        /// <returns>Boolean whether specified Order is valid or invalid</returns>
        public bool BeAValidItemOrder(AddToCartEntity addToCartEntity, int UserId, string UserToken)
        {
            bool IsValidRestaurant = _iAddToCartActions.IsOrderItemInStock(addToCartEntity, UserId, UserToken).GetAwaiter().GetResult();
            return IsValidRestaurant;
        }
        /// <summary>
        /// Make a service call to fetch all valid customers and validate
        /// </summary>
        /// <param name="CustomerId">Customer Id</param>
        /// <returns>Boolean whether specified customer is valid or invalid</returns>
        private bool BeAValidCustomer(int CustomerId)
        {
            bool IsValidCustomer = false;

            return IsValidCustomer;
        }
    }
}
