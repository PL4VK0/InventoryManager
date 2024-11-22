using AutoMapper;
using DAL.Abstract;
using DALef.Context;
using DALef.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DALef.DALs
{
    public class OrderDALEF:IOrderDAL
    {
        private readonly IMapper mapper;
        private readonly string connectionString;
        public OrderDALEF(IMapper mapper, string connectionString)
        {
            this.mapper = mapper;
            this.connectionString = connectionString;
        }
        public Order Add(Order order)
        {
            order.Date = DateTime.Now;
            using (var context = new InvManContext(connectionString))
            {
                var tblOrder = mapper.Map<TblOrder>(order);
                context.TblOrders.Add(tblOrder);
                context.SaveChanges();
                order.OrderID = (short)tblOrder.OrderId;
                return order;
            }
        }
        public List<Order> GetAll()
        {
            using (var context = new InvManContext(connectionString))
            {
                var orders = context.TblOrders.ToList();
                return mapper.Map<List<Order>>(orders);
            }
        }
        public short DeleteByID(short id)
        {
            using (var context = new InvManContext(connectionString))
            {
                var order = context.TblOrders.Where(o=>o.OrderId== id).FirstOrDefault();
                if (order == null)
                    return 0;
                context.TblOrders.Remove(order);
                context.SaveChanges();
            }
            return 0;
        }

        public Order GetByID(short id)
        {
            using (var context = new InvManContext(connectionString))
                return mapper.Map<Order>(context.TblOrders.Where(t=>t.OrderId== id).FirstOrDefault());
        }

        public void Update(Order order)
        {
            order.Date = DateTime.Now;
            using (var context = new InvManContext(connectionString))
            {
                var tblOrder = mapper.Map<TblOrder>(order);
                context.TblOrders.Update(tblOrder);
                context.SaveChanges();
                order.OrderID = (short)tblOrder.OrderId;
            }
        }
    }
}
