const HDWalletProvider = require('truffle-hdwallet-provider');
const fs = require('fs');
module.exports = {
  networks: {
    development: {
      host: "127.0.0.1",
      port: 8545,
      network_id: "*"
    },
    testmarketplace: {
      network_id: "*",
      gas: 0,
      gasPrice: 0,
      provider: new HDWalletProvider(fs.readFileSync('c:\\Users\\edgoode\\Projects\\Blockchain-Marketplace-Accelerator\\01_Quorum_Deployment\\Smart_Contract\\mnemonic.env', 'utf-8'), "https://testcontosomember.blockchain.azure.com:3200/-7wa5-gOITfKI7cViIcLRdSX"),
      consortium_id: 1558630384705,
      type: "quorum"
    },
    testmarketplace02: {
      network_id: "*",
      gas: 0,
      gasPrice: 0,
      provider: new HDWalletProvider(fs.readFileSync('c:\\Users\\edgoode\\Projects\\Blockchain-Marketplace-Accelerator\\01_Quorum_Deployment\\Smart_Contract\\mnemonic2.env', 'utf-8'), "https://testcontoso02.blockchain.azure.com:3200/RCOrbJV-XLTqJDcDTeI8jzgB"),
      consortium_id: 1560541322723,
      type: "quorum"
    }
  },
  mocha: {},
  compilers: {
    solc: {
      version: "0.5.9"
    }
  }
};
