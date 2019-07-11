pragma solidity ^0.5.1;
contract ApplesContract {
  mapping (address => uint) apples;
 
  function addApples(uint toAdd) public returns(bool) {
    apples[msg.sender] = getApples(msg.sender) + toAdd;
    return true;
  }
 
  function getApples(address addr) public view returns(uint) {
    return apples[addr];
  }
}