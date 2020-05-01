import React, { FC, useRef, useState, useEffect } from 'react';
import {
    Card,
    Input,
    List,
    Radio,
} from 'antd';
import styles from './style.less'
import { IChemical, IClaimForm } from '@/models/entity';
import { history, Dispatch } from 'umi';

const ListContent = ({
    data: { state },
}: {
    data: IChemical;
}) => (
        <div className={styles.listContent}>
            <div className={styles.listContentItem}>
                <span>表单状态</span>
                <p>{state}</p>
            </div>
        </div>
    );

export const ClaimFormList = (param: {
    cform?: IClaimForm[],
    dispatch: Dispatch,
}) => {
    const { cform, dispatch } = param;

    const paginationProps = {
        showSizeChanger: true,
        showQuickJumper: true,
        pageSize: 5,
        total: cform?.length || 0,
    };

    return (
        <div className={styles.standardList}>
            <List
                size="large"
                rowKey="id"
                pagination={paginationProps}
                dataSource={cform}
                renderItem={(item) => (
                    <List.Item
                        actions={[
                            <a
                                key="edit"
                                onClick={(e) => {
                                    history.push('/my/process/claim/' + item.id);
                                    dispatch({
                                        type: 'myFormToProcess/updateNotifyStatus',
                                        payload: {
                                            rid: item.id,
                                            type: "claimform"
                                        }
                                    })
                                }}
                            >
                                查看申请详情
                            </a>,
                        ]}
                    >
                        <List.Item.Meta
                            title={<span>{'申请表编号：' + item.id}</span>}
                            description={<div>
                                <span>所属实验室：</span>
                                <span>{item.lid}</span>
                            </div>}
                        />
                        <ListContent data={item} />
                    </List.Item>
                )}
            />
        </div>
    );
};