
import { environment } from '../environments/environment';
import { AuthConfig } from 'app/angular-oauth2-oidc/auth.config';

export function authConfig(url: string): AuthConfig {

    return {
        // Url of the Identity Provider
        issuer: url,
        // URL of the SPA to redirect the user to after login
        redirectUri: url,
        // The SPA's id. The SPA is registered with this id at the auth-server
        clientId: 'lustspa',
        tokenEndpoint: url + 'connect/token',
        loginUrl: url +'connect/authorize',
        requireHttps: environment.production,
        // set the scope for the permissions the client should request
        // The first three are defined by OIDC.
        scope: 'openid profile email offline_access client_id roles',

        sessionChecksEnabled: true
        
         
    };

}
