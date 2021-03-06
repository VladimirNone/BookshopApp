import { AppApiPaths } from './AppConstants';

export class AuthorizeService {
    _user = null;
    _isAuthenticated = false;

    async isAuthenticated() {
        const user = await this.getUser();
        return !!user.username;
    }

    async getUser() {
        if (this._user) {
            return this._user;
        }

        let response = await fetch(AppApiPaths.IsUserAuthenticated, { method: 'POST' });
        const user = await response.json();

        this.updateState(user);

        return user;
    }

    updateState(user) {
        this._user = user;
        this._isAuthenticated = !!this._user.username;
    }

    async checkPermission(onSuccess, onError) {
        let response = await fetch(AppApiPaths.Permission, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            }
        });

        if (!response.ok) {
            onError(await response.json());
        }
        else {
            onSuccess(await response.json());
        }
    }

    async signup(login, password, onSuccess, onError) {
        let response = await fetch(AppApiPaths.Register, {
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
        let response = await fetch(AppApiPaths.Login, {
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
        await fetch(AppApiPaths.Logout, { method: 'POST'});
    }

    static get instance() { return authService }
}

const authService = new AuthorizeService();

export default authService;
