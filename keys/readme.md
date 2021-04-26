This directory contains sample keys you may use with the SDK.
However, you are encouraged to generate and use your own.

## RSA Keys

The public/private key pair were generated using the widely
available `openssl` command.  The private key was generated
using the `genrsa` subcommand.

```
openssl genrsa -out cjissdk-key.pem 2048
```

The certificate was generated with the `req` subcommand.

```
openssl req -x509 -new -key cjissdk-key.pem -out cjissdk-cert.pem -days 3650
```

This command is interactive.  The responses are shown below.

```
Country Name (2 letter code) [AU]:US
State or Province Name (full name) [Some-State]:CA
Locality Name (eg, city) []:County of Los Angeles
Organization Name (eg, company) [Internet Widgits Pty Ltd]:ISAB
Organizational Unit Name (eg, section) []:CJIS Tables
Common Name (e.g. server FQDN or YOUR name) []:Client SDK
Email Address []:
```

This shows that it is ok to omit a value, such as `Email Address`
in the above example.  It is also commmon to omit the
`Organizational Unit Name`.  It's simply a matter of preference.

Note that the `CommonName` is **not** a server FQDN.  We are
**not** using this as an SSL certificate and there is no reason
to assign a server name.  Rather, the `CN` field should 
identify your client.

## HMAC Keys

An HMAC (Hashed Message Authentication Code) can be any string.
`My string` is a valid code; but it is not a good one.  A good
source of random characters for an HMAC key is
https://www.grc.com/passwords.htm.
Under the field **63 random alpha-numeric characters** you can
obtain a code like

```
AqN1oB2zPaKT6RnhSjjQe7y0BZ48nDCBLocYj5EVpYA44LJLM8OBNUzWwZLqHj9
```

These codes are good for the following reasons.

* They do not contain symbol characters which might be misinterpreted
  by some programming environments.

* They are not easily remembered if casually viewed by a human.

* They do not require a tool like `openssl` to generate.

The **drawback** to HMAC codes is that the secret must be shared
with the API admins.  In contrast, with the RSA keys, only the
public key is shared with the API admins.  The private key is
never circulated.  This provides better protection for the secret.