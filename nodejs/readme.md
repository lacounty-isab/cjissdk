## Prerequisites

This [Node.js](https://nodejs.org/en/) client is configured in the usual way.

1. Change to the `nodejs` directory.
2. Run `npm install`.

This installs the
[jsonwebtoken](https://www.npmjs.com/package/jsonwebtoken)
and
[axios](https://www.npmjs.com/package/axios)
NPM packages.

## Samples

The following sections describe the Node.js samples included in
this SDK.

### 1. JWT

This program generates JWTs from the keys in the
[keys](../../keys) directory.  This program has no other
dependencies.  It can be run from the `nodejs/src` direcory.

```
node jwt
```

This does **not** invoke an API. It just generates and prints
encoded JWTs.  Notice how much larger the RSA signature is than
the HMAC signature.

### 2. Retrieve a Charge

This is a bare-bones Node.js implementation of an API client for retrieving
a single charge code.

1. Populate `APIKEY` environment variable with a valid API key
   as explained in the [general prerequisites](../readme.md).

2. Change to the `nodejs/src` directory.

3. Run `getCharge.js`.
   ```
   node getCharge
   ```

   By default, this invokes the TEST environment.  You may alter
   the `hostname` option in the code to point elsewhere.

### 3. System to System

The system-to-system sample only works if the target environment is
configured to allow the `workshop` issuer.  Otherwise this returns a
`401` HTTP status code.

1. Populate the `APIKEY` environment variable.

2. Populate the `CJISSDK_SECRET` environment variable.

3. Change to the `nodejs/src` directory.

4. Run `updateCharge.js`:

   ```
   node updateCharge
   ```

A successful run should return
```
update: 333.456ms Response = {"changedRows":1}
```
