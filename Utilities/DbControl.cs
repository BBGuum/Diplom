using TrueDiplom.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace TrueDiplom.Utilities
{
    class DbControl
    {
        public static ObservableCollection<Category> GetCategories()
        {
            using (var ctx = new DbAppContext())
            {
                return new ObservableCollection<Category>(ctx.Categories.OrderBy(p => p.Id).ToList());
            }
        }
        public static ObservableCollection<Product> GetProducts()
        {
            using (var ctx = new DbAppContext())
            {
                return new ObservableCollection<Product>(ctx.Products.Include(p => p.CategoryEntity).OrderBy(p => p.CategoryEntity.Id).ToList());
            }
        }
        public static ObservableCollection<Product> GetProductsForCatologue()
        {
            using (var ctx = new DbAppContext())
            {
                return new ObservableCollection<Product>(ctx.Products.Include(p => p.CategoryEntity).Where(p => p.Count > 0).OrderBy(p => p.Id).ToList());
            }
        }
        public static ObservableCollection<User> GetUsers()
        {
            using (var ctx = new DbAppContext())
            {
                return new ObservableCollection<User>(ctx.Users.Include(p => p.RoleEntity).OrderBy(p => p.Id).ToList());
            }
        }
        public static ObservableCollection<Order> GetOrders()
        {
            using (var ctx = new DbAppContext())
            {
                return new ObservableCollection<Order>(ctx.Orders.Include(p => p.StatusEntity).Include(p => p.UserEntity).OrderByDescending(p => p.DateGet).ToList());
            }
        }
        public static ObservableCollection<Order> GetUserOrders(int userId)
        {
            using (var ctx = new DbAppContext())
            {
                return new ObservableCollection<Order>(ctx.Orders.Include(p => p.StatusEntity).Where(p => p.UserId == userId).OrderByDescending(p => p.DateGet).ToList());
            }
        }
        public static ObservableCollection<OrderStatus> GetOrderStatuses()
        {
            using (var ctx = new DbAppContext())
            {
                return new ObservableCollection<OrderStatus>(ctx.OrderStatuses.OrderBy(p => p.Id).ToList());
            }
        }
        public static string GetCategoryName(int categoryId)
        {
            using (var ctx = new DbAppContext())
            {
                return ctx.Categories.FirstOrDefault(p => p.Id == categoryId).Name;
            }
        }
        public static User GetUser(int userId)
        {
            using (var ctx = new DbAppContext())
            {
                return ctx.Users.FirstOrDefault(p => p.Id == userId);
            }
        }
        public static Product GetProduct(int prodId)
        {
            using (var ctx = new DbAppContext())
            {
                return ctx.Products.FirstOrDefault(p => p.Id == prodId);
            }
        }
        public static int GetOrderId(DateTime dateTime)
        {
            using (var ctx = new DbAppContext())
            {
                return ctx.Orders.FirstOrDefault(p => p.DateGet == dateTime).Id;
            }
        }

        public static Category GetCategory(int id)
        {
            using (var ctx = new DbAppContext())
            {
                return ctx.Categories.FirstOrDefault(c => c.Id == id);
            }
        }
        public static void AddProduct(Product product)
        {
            using (var ctx = new DbAppContext())
            {
                ctx.Products.Add(product);
                ctx.SaveChanges();
            }
        }
        public static void AddOrder(Order order)
        {
            using (var ctx = new DbAppContext())
            {
                ctx.Orders.Add(order);
                ctx.SaveChanges();
            }
        }
        public static void UpdateProduct(Product product)
        {
            using (var ctx = new DbAppContext())
            {
                Product _product = ctx.Products.FirstOrDefault(p => p.Id == product.Id);
                if (_product != null)
                {
                    _product.Name = product.Name;
                    _product.Price = product.Price;
                    _product.Definition = product.Definition;
                    _product.CategoryId = product.CategoryId;
                    _product.Count = product.Count;
                    if (!string.IsNullOrEmpty(product.ImageSource))
                    {
                        _product.ImageSource = product.ImageSource;
                    }
                    ctx.SaveChanges();
                }
            }
        }
        public static void UpdateUser(User user)
        {
            using (var ctx = new DbAppContext())
            {
                User _user = ctx.Users.FirstOrDefault(p => p.Id == user.Id);
                if (_user != null)
                {
                    _user.Name = user.Name;
                    _user.Phone = user.Phone;
                    _user.Password = user.Password;
                    if(!string.IsNullOrEmpty(user.ImageSource))
                    {
                        _user.ImageSource = user.ImageSource;

                    }
                    ctx.SaveChanges();
                }
            }
        }
        public static void UpdateOrder(Order order)
        {
            using (var ctx = new DbAppContext())
            {
                Order _order = ctx.Orders.FirstOrDefault(a => a.Id == order.Id);
                if (_order != null)
                {
                    _order.StatusId = order.StatusId;
                }
                ctx.SaveChanges();
            }
        }
        public static void AddUser(User user)
        {
            using (var ctx = new DbAppContext())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }
        public static void AddCartProduct(Cart cart)
        {
            using (var ctx = new DbAppContext())
            {
                ctx.Carts.Add(cart);
                ctx.SaveChanges();
            }
        }
        public static void AddOrderProduct(OrderProduct orderProduct)
        {
            using (var ctx = new DbAppContext())
            {
                ctx.OrderProduct.Add(orderProduct);
                ctx.SaveChanges();
            }
        }
        public static void PlusCountOfProducts(Cart cart)
        {
            using (var ctx = new DbAppContext())
            {
                Cart _cart = ctx.Carts.FirstOrDefault(p => p.UserId == cart.UserId && p.ProductId == cart.ProductId);
                if (_cart != null)
                {
                    _cart.Count += 1;
                }
                ctx.SaveChanges();
            }
        }
        public static void MinusCountOfProducts(Cart cart)
        {
            using (var ctx = new DbAppContext())
            {
                Cart _cart = ctx.Carts.FirstOrDefault(p => p.UserId == cart.UserId && p.ProductId == cart.ProductId);
                if (_cart != null)
                {
                    if (_cart.Count > 1)
                    {
                        _cart.Count--;
                    }
                    else
                    {
                        DeleteProductFromCart(cart);
                    }
                }
                ctx.SaveChanges();
            }
        }
        public static void DeleteProductFromCart(Cart cart)
        {
            using (var ctx = new DbAppContext())
            {
                ctx.Remove(cart);
                ctx.SaveChanges();
            }
        }
        public static void DeleteUser(User user)
        {
            using (DbAppContext db = new DbAppContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
        public static ObservableCollection<Cart> GetAllProductsInCart(int UserId)
        {
            using (var db = new DbAppContext())
            {
                return new ObservableCollection<Cart>(db
                    .Carts
                    .Include(p => p.UserEntity)
                    .Where(p => p.UserId == UserId)
                    .Include(p => p.ProductEntity)
                    .OrderBy(p => p.ProductId)
                    .ToList());
            }
        }
        public static ObservableCollection<OrderProduct> GetAllProductsInOrder(int orderId)
        {
            using (var db = new DbAppContext())
            {
                return new ObservableCollection<OrderProduct>(db
                    .OrderProduct
                    .Include(p => p.OrderEntity)
                    .ThenInclude(p => p.UserEntity)
                    .Where(p => p.OrderId == orderId)
                    .Include(p => p.ProductEntity)
                    .OrderBy(p => p.ProductId)
                    .ToList());
            }
        }
        public static User? IsAuthorized(string login, string password)
        {
            using (var ctx = new DbAppContext())
            {
                return ctx.Users
                    .FirstOrDefault(p => p.Login == login && p.Password == password);
            }
        }
        public static bool IsNameExist(string login)
        {
            using (var ctx = new DbAppContext())
            {
                return ctx.Users.Any(u => u.Login == login);
            }
        }
        public static Product? IsProductExist(string name)
        {
            using (var ctx = new DbAppContext())
            {
                return ctx.Products.FirstOrDefault(p => p.Name == name);
            }
        }
    }
}
