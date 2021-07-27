import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Logout } from './components/Logout';
import { Signup } from './components/Signup';
import { MainPage } from './components/MainPage';
import { PrivateRoute } from './components/api-authorization/PrivateRoute';
/*import { Product } from './components/Product';
import { Basket } from './components/Basket';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';*/
import { ApplicationPaths } from './components/api-authorization/ApiConstants';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={MainPage} />
                <Route path={ApplicationPaths.Login} component={Login} />
                <PrivateRoute path={ApplicationPaths.Logout}>
                    <Logout />
                </PrivateRoute>
                <Route path={ApplicationPaths.Register} component={Signup} />
                {/*        <Route path='/product' component={Product} />
                    <Route path='/basket' component={Basket} />*/}
            </Layout>
        );
    }
}
