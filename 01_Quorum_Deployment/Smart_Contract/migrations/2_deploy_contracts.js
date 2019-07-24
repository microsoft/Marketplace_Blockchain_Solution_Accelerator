var Utils = require('web3-utils')
var Marketplace = artifacts.require("Marketplace");

var orderCode = Utils.asciiToHex("test"); 
var orderStatus = 0; 
var bookingDate = Utils.asciiToHex("test");
var serviceType = 0;
var serviceDate = Utils.asciiToHex("test");
var supplier = Utils.asciiToHex("test");
var quantity = Utils.asciiToHex("test");
var price = Utils.asciiToHex("test");
var currency = Utils.asciiToHex("test");
module.exports = deployer => {
    deployer.deploy(Marketplace, orderCode, orderStatus, bookingDate, serviceType, serviceDate, supplier, quantity, price, currency);
};