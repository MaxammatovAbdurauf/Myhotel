﻿using MyhotelApi.Database.IRepositories;

namespace MyhotelApi.Database.ConcreteTypeRepositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
    }

    private IUserRepository? _userRepository { get; set; }
    public IUserRepository userRepository
    {
        get
        {
            if (_userRepository is null) _userRepository = new UserRepository(context);
            return _userRepository;
        }
    }

    private IHouseRepository? _houseRepository { get; set; }
    public IHouseRepository houseRepository
    {
        get
        {
            if (_houseRepository is null) _houseRepository = new HouseRepository(context);
            return _houseRepository;
        }
    }

    private IRoomRepository? _roomRepository { get; set; }
    public IRoomRepository roomRepository
    {
        get
        {
            if (_roomRepository is null) _roomRepository = new RoomRepository(context);
            return _roomRepository;
        }
    }

    private IReservationRepository? _reservationRepository { get; set; }
    public IReservationRepository reservationRepository
    {
        get
        {
            if (_reservationRepository is null) _reservationRepository = new ReservationRepository(context);
            return _reservationRepository;
        }
    }

    private IReviewRepository? _reviewRepository { get; set; }
    public IReviewRepository reviewRepository
    {
        get
        {
            if (_reviewRepository is null) _reviewRepository = new ReviewRepository(context);
            return _reviewRepository;
        }
    }

    public int Save()
    {
        return 0;
    }
}