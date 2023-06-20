### TODO:

***- add property file***

***- improve reporting***

### LOGIN DATA:
- EMAIL-ADRESSE: max.musterman.postat.test@gmail.com
- PASSWORT: postATtest2023!
- KUNDENNUMMER: 5295007

### MORE POSSIBLE TESTS:

**Positive Tests (Valid Credentials):**

> Successful login after resetting the password in "Login & Einstellungen" with a new password.
> Successful login after resetting the password in Login Page by clicking "Passwort vergessen?" and resetting to a new password.

**Negative Tests (Invalid Credentials):**

>Login attempt with a locked or disabled user account.

>Login attempt with an expired password (if it possible).

>Login attempt with SQL injection and similar security testing in the username/email and password fields.