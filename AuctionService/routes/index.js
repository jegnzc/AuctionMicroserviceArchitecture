var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function (req, res, next) {
  res.render('index', { title: 'Express' });
});

module.exports = router;

// Display all Auctions
router.get('/auctions', (request, response) => {
  client.trackTrace("Loading all auctions");
  pool.query('SELECT idAuction, Name, Description, StartingPrice, AuctionDate,Status, Image, userName, DATEDIFF(date_add(auctiondate, intervalactiveinhours hour), curdate()) * 24 as ActiveInHours, BidPricefrom auctionservicedb.auction where DATEDIFF(date_add(auctiondate, interval activeinhourshour),curdate()) >=0 and IsActive=1', (error, result) => {
    if (error) throw error;
    response.send(result);
  });
});

// Display all Auctions
router.get('/auctions', (request, response) => {
  client.trackTrace("Loading all auctions");
  pool.query('SELECT idAuction, Name, Description, StartingPrice,AuctionDate,Status, Image, userName, DATEDIFF(date_add(auctiondate, intervalactiveinhours hour), curdate()) * 24 as ActiveInHours, BidPricefrom auctionservicedb.auction where DATEDIFF(date_add(auctiondate, interval activeinhourshour),curdate()) >=0 and IsActive=1', (error, result) => {
    if (error) throw error;
    response.send(result);
  });
});
