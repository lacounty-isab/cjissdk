const axios = require('axios');
const { rsaToken, hmacToken } = require('./jwt');

const apikey = process.env['APIKEY'];
if (apikey) {
  console.log('Read API key from APIKEY environment variable');
} else {
  console.error('Failed to read APIKEY environment variable.');
}

const url = 'https://api-test.codes.lacounty-isab.org/api/ChargeCode'

const headers = {
  'x-api-key': apikey,
  'Authorization': `Bearer ${rsaToken}`
}

const data = {
  id: 826,
  short_description: 'UNLAWFUL LIQUOR SALES 1'
};

const run = async () => {
  console.time('update');
  try {
    const res = await axios.post(url, data, { headers });
    if (res.status === 200) {
      console.timeLog('update', `Response = ${JSON.stringify(response.data)}`)
    } else {
      console.timeLog('update', `Received status code ${res.status}`);
    }
  } catch (err) {
    console.timeLog('update', err.message);
  }
}

run();