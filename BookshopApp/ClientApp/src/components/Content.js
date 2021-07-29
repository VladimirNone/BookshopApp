import React, { Component } from 'react';
import { MainPage } from './ContentItems/MainPage';
import { Product } from './ContentItems/Product';
import { Cart } from './ContentItems/Cart';
import { Route, Switch } from 'react-router';
import { ApplicationPagePaths } from './Api-authorization/AppConstants';

export class Content extends Component {

    render() {
        return (
            <div className="row">
                <div className="col-md border">
                    Категории
			    </div>

                <div className="col-md-7 border">
                    <Switch>
                        <Route exact path='/' component={MainPage} />
                        <Route path={ApplicationPagePaths.Product + '/:id'} component={Product} />
                        <Route path={ApplicationPagePaths.Cart} component={Cart} />
                    </Switch>
                </div>

                <div className="col-md border">
                    <div className="row border">one</div>
                    <div className="row border">two</div>
                </div>
            </div>);
    }
}