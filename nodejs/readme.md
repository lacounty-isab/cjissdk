## Prerequisites

This [Node.js](https://nodejs.org/en/) client is configured in the usual way.

1. Change to the `nodejs` directory.
2. Run `npm install`.

This installs the
[jsonwebtoken](https://www.npmjs.com/package/jsonwebtoken)
NPM package.

## Samples

The following sections describe the Node.js samples included in
this SDK.

### 1. JWT

Generate a JWT by running `jwt.js`.

```
node jwt
```

By default it will use the keys in [keys](../../keys).
This does **not** invoke an API.

### 2. Retrieve a Charge

This is a bare-bones Node.js implementation of an API client for retrieving
a single charge code.

1. Populate `APIKEY` environment variable with a valid API key
   as explained in the [general prerequisites](../readme.md).

2. Run `getCharge.js`.
   ```
   node getCharge
   ```

   By default, this runs against the TEST environment.  You may alter
   the `hostname` option in the code to point elsewhere.

### 3. System to System

The system-to-system sample only works if the target environment is
configured to allow the `myidp` issuer.  Otherwise this returns a
`401` HTTP status code.