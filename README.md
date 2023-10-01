# SystemIntegrationMP1
Frederik, Julius Janus

### The solution

In this mini project, we send emails to a list of recipients based on their name, email address, and IP address.

The email should salute the recipient with either Ms. or Mr.

To do this, we need to use external APIs and web services to get the gender of a recipient based on their name and country.

First approach is to use the IP address of the recipient to get the country. Here we used a SOAP API. This code is located in CountryService: `FindCountryByIpAsync(string ip)`

Secondly, knowing the country and the name, we can use an external gender API to get the gender of a recipient based on their name and country. This is a REST API and is found in GenderService: `FindGenderByNameAndCountryAsync(Recipient recipient)`

Finally, we can send the email in EmailService using the genders from GenderService.

The code in each service is executed in a controller using HTTP requests.


![EmailSent](https://github.com/FrederikBA/SystemIntegrationMP1/assets/61831295/5165db8b-137b-401d-bb3c-1a66eefb585d)
