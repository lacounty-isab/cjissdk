const axios = require('axios');
const jwt = require('jsonwebtoken');

const ENV_APIKEY = 'APIKEY';
const ENV_SECRET = 'CJISIDP_SECRET';

const apikey = process.env[ENV_APIKEY];
if (apikey) {
  console.log(`Read API key from ${ENV_APIKEY} environment variable`);
} else {
  console.error(`Failed to read ${ENV_APIKEY} environment variable`);
  process.exit(1);
}

const hmacSecret = process.env['CJISSDK_SECRET'];
if (hmacSecret) {
  console.log(`Read HMAC secret from ${ENV_SECRET} environment variable`);
} else {
  console.error(`Failed to read ${ENV_SECRET} environment variable`);
  process.exit(2);
}

const hmacOptions = {
  algorithm: 'HS256',
  expiresIn: '2h',
  issuer: 'workshop',
};

const claims = {
  sub: 'e123123',
  aud: 'cjisapi',
  enabled: true,
  scope: ['da', 'lasd', 'shortdesc'],
}

const encodedJwt = jwt.sign(claims, hmacSecret, hmacOptions);
console.log(`Encoded JWT:`);
console.log(encodedJwt);
console.log();

const url = 'https://api-test.codes.lacounty-isab.org/api/ChargeCode'

const headers = {
  'x-api-key': apikey,
  'Authorization': `Bearer ${encodedJwt}`
}

const data = {
  id: 826,
  short_description: 'UNLAWFUL LIQUOR SALES 2'
};

const run = async () => {
  console.time('update');
  try {
    const res = await axios.patch(url, data, { headers });
    if (res.status === 200) {
      console.timeLog('update', `Response = ${JSON.stringify(res.data)}`)
    } else {
      console.timeLog('update', `Received status code ${res.status}`);
    }
  } catch (err) {
    console.timeLog('update', err.message);
    if (error.response) {
      console.log(`Status code: ${err.response.status}`);
      console.log(`Response: ${JSON.stringify(err.response.data)}`);
    }
  }
}

run();