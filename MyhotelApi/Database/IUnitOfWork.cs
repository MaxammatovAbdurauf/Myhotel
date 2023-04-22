using MyhotelApi.Database.ConcreteTypeRepositories;

namespace MyhotelApi.Database.IRepositories;

public interface IUnitOfWork
{
    public IUserRepository userRepository { get;}
    public IHouseRepository houseRepository { get;}
    public IRoomRepository roomRepository { get;}
    public IReservationRepository reservationRepository { get;}
    public IReviewRepository reviewRepository { get;}   
    int Save();
}