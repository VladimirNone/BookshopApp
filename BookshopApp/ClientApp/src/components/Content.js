import React, { Component } from 'react';
import { MainPage } from './ContentItems/MainPage/MainPage';
import { Product } from './ContentItems/Product';
import { Basket } from './ContentItems/Basket';
import { Route } from 'react-router';

export class Content extends Component {

    render() {
        return (
            <div className="row">
                <div className="col-md border">
                    Категории
			        </div>

                <div className="col-md-7 border">

                    <Route exact path='/' component={MainPage} />
                    <Route path='/product' component={Product} />
                    <Route path='/basket' component={Basket} />
                </div>

                <div className="col-md border">
                    <div className="row border">one</div>
                    <div className="row border">two</div>
                </div>
            </div>);
    }
}