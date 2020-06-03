import { Button, Result, Descriptions, Statistic } from 'antd';
import React from 'react';
import { connect, Dispatch, history } from 'umi';
import { PostFinancialFormStateType } from '../../model';
import styles from './index.less';

interface Step3Props {
  data: PostFinancialFormStateType;
  dispatch: Dispatch;
}

const Step3: React.FC<Step3Props> = (props) => {
  const { data, dispatch } = props;
  if (!data.formdata) {
    return null;
  }
  const onFinish = () => {
    if (dispatch) {
      dispatch({
        type: 'postFinancialForm/clearFormData',
      });
    }
  };
  const information = (
    <div className={styles.information}>
      <Descriptions column={1}>
        <Descriptions.Item label="所属申请编号"> {data.formdata?.wid}</Descriptions.Item>
        <Descriptions.Item label="收款方"> {data.formdata.receiver}</Descriptions.Item>
        <Descriptions.Item label="金额">{data.formdata.price}
        </Descriptions.Item>
      </Descriptions>
    </div>
  );
  const extra = (
    <>
      <Button type="primary" onClick={onFinish}>
        继续申请
      </Button>
      <Button onClick={(e) => {
        e.preventDefault();
        history.push('/my/workflow/' + data.formdata?.wid);
        dispatch({
          type: 'postFinancialForm/clearFormData',
        })
      }}>查看账单</Button>
    </>
  );
  return (
    <Result
      status="success"
      title="操作成功"
      extra={extra}
      className={styles.result}
    >
      {information}
    </Result>
  );
};

export default connect(({
  postFinancialForm,
}: {
  postFinancialForm: PostFinancialFormStateType,
}) => ({
  data: postFinancialForm,
}))(Step3);
