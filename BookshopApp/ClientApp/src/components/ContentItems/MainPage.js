import React, { Component, Fragment } from 'react';
import { Searcher } from './MainPageComponents/Searcher';
import { Card } from './MainPageComponents/Card';
import { Pagination } from './MainPageComponents/Pagination';
import { ApplicationPaths } from '../Api-authorization/ApiConstants';

export class MainPage extends Component {
    constructor(props) {
        super(props);

        this.state = {
            page: 1,
            products: [],
            pageIsLast: false,
            dataWasUpdated: true,
        }

    }

    componentDidMount() {
        this.getProductsFromServer();
    }

    componentDidUpdate() {
        if (!this.state.dataWasUpdated)
            this.getProductsFromServer();
    }

    async getProductsFromServer() {
        //The server pagination starts from zero
        let response = await fetch(ApplicationPaths.Products + '/' + (this.state.page - 1), {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok)
        {
            console.log(await response.json())
        }
        else
        {
            const answer = await response.json();
            this.setState({ products: answer.prods, pageIsLast: answer.pageIsLast, dataWasUpdated: true });
        }
    }

    render() {
        const prods = this.state.products;

        return (
            <Fragment>
                <Searcher />
                {prods.map((prod, i) => <Card key={i} product={prod} />)}
                <Pagination curPage={this.state.page} pageIsLast={this.state.pageIsLast} changePage={(newPage) => this.setState({ page: newPage, dataWasUpdated: false })} />
            </Fragment>
        );
    }
}
