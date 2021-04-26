const https = require('https');

const apikey = process.env['APIKEY'];

if (apikey) {
  console.log('Read API key from APIKEY environment variable.');
} else {
  console.error('Failed to find API key from APIKEY environment variable.');
}

const options = {
  hostname: 'api-test.codes.lacounty-isab.org',
  method: 'GET',
  path: '/api/ChargeCode/826',
  headers: { 
    'Accept': 'application/json',
    'x-api-key': apikey
  },
  timeout: 10000
}

console.time('request');
const req = https.request(options);
req.end();
req.on('error', err => {
  console.timeLog('request', `Received client error ${err}`);
  console.timeEnd('request');
});
req.on('response', res => {
  console.timeLog('request', `Initial response returned`);
  const httpStatus = res.statusCode;
  const contentType = res.headers['content-type'];
  console.log(`HTTP Status: ${httpStatus}.  Content-type = ${contentType}.`)

  let error;
  if (httpStatus >= 300) {
    error = new Error(`Request Failed. Status code = ${httpStatus}.`);
  } else if (!/json/.test(res.headers['content-type'])) {
    error = new Error(`Invalid content type: ${contentType}.  Expected json.`);
  }

  if (error) {
    console.log(error.message);
    res.resume();
  } else {
    res.setEncoding('utf8');
    let chunkCount = 0;
    let rawData = '';
    res.on('data', chunk => {
      rawData += chunk;
      chunkCount++;
      console.timeLog('request', `Received chunk ${chunkCount}`);
    });
    res.on('end', () => {
      console.timeLog('request', `Received all chunks`);
      console.log(`Received response in ${chunkCount} chunk(s).`);
      try {
        const jsonRes = JSON.parse(rawData);
        console.log(`Received ${jsonRes.length} objects.`);
        console.log(JSON.stringify(jsonRes, null, 2));
      } catch(e) {
        console.error(`Caught exception during response parsing: ${e.message}`);
      }
    });
  }
});
