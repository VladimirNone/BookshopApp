import { ApplicationPaths, ApplicationName } from './ApiAuthorizationConstants';

export class AuthorizeService {
    _username = null;
    _isAuthenticated = false;

    async isAuthenticated() {
        const user = await this.getUser();
        return !!user.username;
    }

    async getUser() {
        if (this._username) {
            return this._username;
        }

        let response = await fetch(ApplicationPaths.IsUserAuthenticated, { method: 'POST' });
        const username = await response.json();

        return username;
    }

    updateState(user) {
        this._username = user.username;
        this._isAuthenticated = !!this._username;
    }

    async signup(login, password, onError) {
        let response = await fetch(ApplicationPaths.Register, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ Email: login, UserName: login, Password: password })
        });

        if (response.status != 200)
            onError(await response.json());
    }

    static get instance() { return authService }
}

const authService = new AuthorizeService();

export default authService;
