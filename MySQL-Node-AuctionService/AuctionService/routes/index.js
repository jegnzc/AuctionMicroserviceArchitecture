var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function (req, res, next) {
  client.trackTrace("Auction Service running");
  res.render('index', { title: 'Online Auction Service Working!, version: 1.1' });
});

// Load the MySQL pool connection
const pool = require('../public/data/config');

// Display all Auctions
router.get('/auctions', (request, response) => {
  client.trackTrace("Loading all auctions");
  pool.query('SELECT idAuction, Name, Description, StartingPrice, AuctionDate, Status, Image, userName,  DATEDIFF(date_add(auctiondate, interval activeinhours hour), curdate()) * 24  as ActiveInHours, BidPrice  from auctionservicedb.auction where DATEDIFF(date_add(auctiondate, interval activeinhours hour), curdate()) >=0  and IsActive=1', (error, result) => {
    if (error) throw error;
    response.send(result);
  });
});

// Display all Winning Bids for user
router.get('/auctionsbyBidderId/:userId', (request, response) => {
  client.trackTrace("Loading all auctions by user id");
  const userId = request.params.userId;
  pool.query("SELECT * from auction where IsActive=1 and bidUser ='" + userId + "'", (error, result) => {
    if (error) throw error;
    response.send(result);
  });
});

//Create a new Auction Item
router.post('/auctions', (request, response) => {
  client.trackTrace("Creating auction");
  console.log(request.body);
  pool.query('INSERT INTO auction SET ?', request.body, (error, result) => {
    if (error) throw error;

    response.send(result.insertId.toString());
  });
});

//Update auction data sent through Kafka consumer for topic: bidtopic
router.put('/updateAuctionForBid', (request, response) => {
  client.trackTrace("Updating auction for bid");
  console.log("bid id is " + request.body.bidid);
  console.log("bidPrice is " + request.body.bidAmount);
  console.log("auctionId is " + request.body.auctionId);
  console.log("userId is " + request.body.userId);
  pool.query("UPDATE auction set bidId='" + request.body.bidid + "', bidPrice=" + request.body.bidAmount + ", bidUser='" + request.body.userId + "' where idAuction=" + request.body.auctionId, (error, result) => {
    if (error) throw error;
    response.send("updated");
  });
});

//Get list of Auctions based on Customer Id
router.get('/auctionsByUserId/:userId', (request, response) => {
  client.trackTrace("Loading auction by User ID");
  console.log(request.body);
  const userId = request.params.userId;
  pool.query("SELECT * FROM auction where userId ='" + userId + "'", (error, result) => {
    if (error) throw error;
    response.send(result);
  });
});

//Get list of Auctions based on Auction Id
router.get('/auctionById/:id', (request, response) => {
  client.trackTrace("Loading all auction by User ID");
  console.log(request.body);
  const id = request.params.id;
  pool.query('SELECT name, description,startingPrice, bidPrice, auctionDate, status, image, activeInHours, userId, userName, idAuction FROM auction where idauction =' + id, (error, result) => {
    if (error) throw error;
    response.send(result);
  });
});


module.exports = router;

