const fs = require('fs');
const path = require('path');
const jwt = require('jsonwebtoken');

const hmacKeyPath = path.join(__dirname, '..', '..', 'keys', 'cjissdk-hmac.txt');
const pubKeyPath  = path.join(__dirname, '..', '..', 'keys', 'cjissdk-cert.pem');
const privKeyPath = path.join(__dirname, '..', '..', 'keys', 'cjissdk-key.pem');

//---------- RSA Signature --------------------------

const payload = {
  scopes: ['da', 'lasd'],
  user: 'e123123'
}

const privKey = fs.readFileSync(privKeyPath);

const rsaOptions = {
  algorithm: 'RS256',
  expiresIn: '2h',
  issuer: 'myidp',
};

console.time('jwt');
console.timeLog('jwt', `About to sign JWT with private key from ${privKeyPath}.`);
const rsaToken = jwt.sign(payload, privKey, rsaOptions);

console.timeLog('jwt', `Signed RSA rsaToken length: ${rsaToken.length}`);
console.log(rsaToken);
console.log();

console.timeLog('jwt', `About to verify JWT with public key from ${pubKeyPath}.`);
const pubKey = fs.readFileSync(pubKeyPath);
let verifyOptions = {
  algorithms: ['RS256'],
}
let decoded = jwt.verify(rsaToken, pubKey, verifyOptions);
console.timeLog('jwt', `Decoded: ${JSON.stringify(decoded, null, 2)}`);
console.log()

//-------- HMAC Signature -----------------------

const hmOptions = {
  algorithm: 'HS256',
  expiresIn: '2h',
  issuer: 'myidp'
}

const hmacKey = fs.readFileSync(hmacKeyPath);
console.timeLog('jwt', `About to sign JWT with HMAC key from ${hmacKeyPath}`)
const hmacToken = jwt.sign(payload, hmacKey, hmOptions);

console.timeLog('jwt', `Signed HMAC rsaToken length: ${hmacToken.length}`);
console.log(hmacToken);
console.log();

decode = jwt.verify(hmacToken, hmacKey, hmOptions);
console.timeLog('jwt', `Decoded ${JSON.stringify(decoded, null, 2)}`);

console.timeEnd('jwt');

module.exports = {rsaToken, hmacToken }