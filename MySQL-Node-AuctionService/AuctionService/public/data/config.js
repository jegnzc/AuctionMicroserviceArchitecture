const mysql = require('mysql');
// Set database connection credentials
const config = {
host: 'mysql-oas-dev.mysql.database.azure.com',
user: 'mysqladmin@mysql-oas-dev',
password: 'P@ssw0rd!@#',
database: 'auctionservicedb',
};
// Create a MySQL pool
const pool = mysql.createPool(config);
// Export the pool
module.exports = pool;