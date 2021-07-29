import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Logout } from './components/Logout';
import { Signup } from './components/Signup';
import { ApplicationPagePaths } from './components/Api-authorization/AppConstants';

import './custom.css'
import { Content } from './components/Content';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Switch>
                    <Route path={ApplicationPagePaths.Login} component={Login} />
                    <Route path={ApplicationPagePaths.Logout} component={Logout} />
                    <Route path={ApplicationPagePaths.Register} component={Signup} />

                    <Route path="/" component={Content}/>
                </Switch>
            </Layout>
        );
    }
}
