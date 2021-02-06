# Loan Compliance Api

This application checks loan data encoded in JSON format against known compliance rules.

## Running this application
1. To run, open Visual Studio and launch the application (should usually launch on port 5000)
1. Then, navigate to the Swashbuckle UI page (or let the application navigate automatically there for you):
http://localhost:5000/index.html
1. Select the POST endpoint /Loan/process, and click on the button 'Try it out'
1. Enter a request into the box and click 'Execute'

## Sample Request

For the /Loan/process endpoint (a POST request), here's an example JSON body:

```json
{
  "loanAmount": 1000000,
  "annualPercentageRate": 7.8,
  "state": "Virginia",
  "loanType": "Conventional",
  "occupancyType": "PrimaryOccupancy",
  "feeAllocations": [
    {
      "loanFeeType": "FloodCertification",
      "feeCharged": 100000
    },
    {
        "loanFeeType": "CreditReport",
        "feeCharged": 100000
    }
  ]
}
```