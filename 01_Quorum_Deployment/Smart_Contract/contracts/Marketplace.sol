pragma solidity ^0.5.9;

contract Marketplace {

    enum ConsignmentMilestone { 
        PLACE_ORDER, 
        AWAITING_SUPPLIER_APPROVAL, 
        SUPPLIER_APPROVED, 
        ACTIVITY_INPROGRESS,
        ACTIVITY_COMPLETED, 
        READY_FOR_AP 
    }

    enum ServiceType{
	TRANSPORT,
	ACTIVITY,
	ACCOMODATION
    }

    enum Validator{
        Contoso,
        Supplier
    }

    struct Party{
        string partyId;
        address accountAddress;
    }

    struct Consignment {
        string orderCode; 
        ConsignmentMilestone orderStatus; 
        string bookingDate;
        ServiceType serviceType;
        string serviceDate;
        Party supplier;
        string quantity;
        string price;
        string currency;
    }

    Party public contoso;


    mapping (string => Consignment) private consignmentCollection;

    function setMilestone(
        string memory bindingID,
        ConsignmentMilestone _milestone
    ) public returns (bool success) {
        consignmentCollection[bindingID].orderStatus = _milestone;
        return true;
    }

    function NeoPlaceOrder(
        string memory _bindingID,
        string memory _orderCode,
        ConsignmentMilestone _orderStatus,
        string memory _bookingDate,
        ServiceType _serviceType,
        string memory _serviceDate,
        string memory _partyID,
        string memory _quantity,
        string memory _price,
        string memory _currency
 
    ) public returns(bool success) 
    {
        Consignment memory consignment = Consignment({
        supplier : Party({
            partyId : _partyID,
            accountAddress : msg.sender
        }),
        orderCode : _orderCode,
        orderStatus : _orderStatus,
        bookingDate : _bookingDate,
        serviceType : _serviceType,
        serviceDate : _serviceDate,
        quantity : _quantity,
        price : _price,
        currency : _currency
        });

        consignmentCollection[_bindingID] = consignment;
        return true;
    }

    function getConsignment(string memory _bindingID) public view returns(ConsignmentMilestone value){
        return consignmentCollection[_bindingID].orderStatus;
    }
}