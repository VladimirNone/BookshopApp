import { ApplicationPaths } from './ApiAuthorizationConstants';
import { $, ajax } from 'jquery';

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
        const user = await response.json();

        return user;
    }

    updateState(user) {
        this._username = user.username;
        this._isAuthenticated = !!this._username;
    }

    async signup(login, password, onSuccess, onError) {
        let response = await fetch(ApplicationPaths.Register, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ Email: login, UserName: login, Password: password })
        });

        if (!response.ok)
            onError(await response.json());
        else
            onSuccess();

    }

    async login(login, password, onSuccess, onError) {
        let response = await fetch(ApplicationPaths.Login, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ Login: login, Password: password })
        });

        if (!response.ok)
            onError(await response.json());
        else
            onSuccess();
    }

    async logout() {
        await fetch(ApplicationPaths.Logout, {method: 'POST'});
    }

    static get instance() { return authService }
}

const authService = new AuthorizeService();

export default authService;
