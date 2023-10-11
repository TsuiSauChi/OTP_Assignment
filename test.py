import requests
import time

url = 'http://localhost:5001/api/users'

response = requests.get(url)

if response.status_code == 200:
    print("Connection Successful")
    print(response.text)
    print()

    print()
    id = "1"
    email = "tsuisauchi.tsc@gmail.com"
    url = "http://localhost:5001/api/user/email?id=" + id +"&user_email=" + email
    response = requests.post(url)

    print("Sending email with incorrect format")
    print("Example: tsuisauchi.tsc@gmail.com")
    print(response.text)
    print()

    id = "1"
    email = "tsuisauchi.tsc.dso.org.sg"
    url = "http://localhost:5001/api/user/email?id=" + id +"&user_email=" + email
    otp = requests.post(url)
    
    print("Sending email with correct format")
    print("Example: tsuisauchi.tsc.dso.org.sg")
    print(otp.text)
    print()

    id = "1"
    wrongOtp = "999999"
    url = "http://localhost:5001/api/user/checkotp?id=" + id +"&otp=" + wrongOtp
    print("Send incorrect otp 11 times")
    for i in range(11):
        response = requests.get(url) 
        print(response.text)
    print()

    print("Send correct otp after 11 incorrect otp")
    url = "http://localhost:5001/api/user/checkotp?id=" + id +"&otp=" + str(otp.text)
    response = requests.get(url) 
    print(response.text)

    print("Send correct otp after 5 second")
    time.sleep(7)
    response = requests.get(url) 
    print(response.text)
    print()

    url = "http://localhost:5001/api/user/email?id=" + id +"&user_email=" + email
    otp = requests.post(url)
    
    print("Sending email with correct format")
    print("Example: tsuisauchi.tsc.dso.org.sg")
    print(otp.text)
    print()
    print("Send correct otp")
    url = "http://localhost:5001/api/user/checkotp?id=" + id +"&otp=" + str(otp.text)
    response = requests.get(url)
    print(response.text)
    print()

else:
    print("Connection Failed")




